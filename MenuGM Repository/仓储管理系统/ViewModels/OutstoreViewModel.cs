using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace 仓储管理系统.ViewModels
{
    internal class OutstoreViewModel : ObservableObject
    {
        // 改为字段--增删改查类
        private OutstoreOrderRepository outstoreOrderRepository = new OutstoreOrderRepository();
        private OutstoreRepository outstoreRepository = new OutstoreRepository();
        private WarehouseRepository warehouseRepository = new WarehouseRepository();
        private CustomerRepository customerRepository = new CustomerRepository();
        private LocationRepository locationRepository = new LocationRepository();
        private CargoRepository cargoRepository = new CargoRepository();



        #region 列表所有项，其他数据没有当前项
        // 当前入库单号
        private OutstoreOrder outstoreOrder = new OutstoreOrder();
        public OutstoreOrder OutstoreOrder
        {
            get => outstoreOrder;
            set => SetProperty(ref outstoreOrder, value);
        }

        /// <summary>
        /// 仓库
        ///    Warehouses 所有可选项--绑定到单选列表的 ItemsSource;
        ///    Warehouse 当前选定项--绑定到 SelectedItem;
        /// </summary>  
        private Warehouse warehouse = new Warehouse();
        public Warehouse Warehouse
        {
            get => warehouse;
            set { SetProperty(ref warehouse, value); }
        }

        private List<Warehouse> warehouses;
        public List<Warehouse> Warehouses
        {
            get => warehouses;
            set { SetProperty(ref warehouses, value); }
        }

        // 供应商->客户  
        private Customer customer = new Customer();
        public Customer Customer
        {
            get => customer;
            set { SetProperty(ref customer, value); }
        }

        private List<Customer> customers;
        public List<Customer> Customers
        {
            get => customers;
            set { SetProperty(ref customers, value); }
        }


        //入库明细
        private Outstore outstore = new Outstore();
        public Outstore Outstore
        {
            get => outstore;
            set { SetProperty(ref outstore, value); }
        }
        // 实体类--集合形式
        // 动态通知 
        //不需对 Instores 判断空
        private ObservableCollection<Outstore> outstores = new ObservableCollection<Outstore>();
        public ObservableCollection<Outstore> Outstores
        {
            get => outstores;
            set { SetProperty(ref outstores, value); }
        }

        // 库位管理  
        //列表--所有
        public List<Location> locations;
        public List<Location> Locations
        {
            get => locations;
            set { SetProperty(ref locations, value); }
        }


        //物资 
        //列表--所有
        public List<Cargo> cargos;
        public List<Cargo> Cargos
        {
            get => cargos;
            set { SetProperty(ref cargos, value); }
        }
        #endregion
        

        public ICommand LoadedCommand { get; }
        //前端 所属仓库变动--相对数据源绑定
        public ICommand WarehouseSelectionChangedCommand { get; }
        public ICommand CreateInstoreCommand { get; }
        public ICommand AddOrUpdateCommand { get; }
        public ICommand CreateCommand { get; }
        public OutstoreViewModel()
        {
            CreateCommand = new RelayCommand(OnCreateCommand);

            LoadedCommand = new RelayCommand(OnLoadedCommand);
            WarehouseSelectionChangedCommand = new RelayCommand<Warehouse>(OnWarehouseSelectionChangedCommand);
            CreateInstoreCommand = new RelayCommand(OnCreateInstoreCommand);
            AddOrUpdateCommand = new RelayCommand(OnAddOrUpdateCommand);

        }

        //=>用户成功后刷新
        private void OnCreateCommand()
        {
            // 1.普通方法(与单元格新建相同)
             Outstores = new ObservableCollection<Outstore>();
             OutstoreOrder = new OutstoreOrder();
             OutstoreOrder.NameEx = Guid.NewGuid().ToString();

            // 2.直接刷新界面
            // 重置所有属性-命令实现 

            // 3.导航服务 
        } 

        private void OnAddOrUpdateCommand()
        {
            // 入库表
            if (OutstoreOrder == null) return;

            if (string.IsNullOrEmpty(OutstoreOrder.Name))
            {
                MessageBox.Show("出库单号不能为空");
                return;
            }

            // 仓库与id
            if (Warehouse == null || Warehouse.Id == 0)
            {
                MessageBox.Show("请选择仓库");
                return;
            }
            else
            {
                //生成单号=仓库id
                OutstoreOrder.WarehouseId = Warehouse.Id;
            }

            //  
            if (Customer == null || Customer.Id == 0)
            {
                MessageBox.Show("请选择客户");
                return;
            }
            else
            {
                OutstoreOrder.CustomerId = Customer.Id;

            }

            if (Outstores == null || Outstores.Count == 0)
            {
                MessageBox.Show("请增加入库的物资");
                return;
            }


            OutstoreOrder.MemberId = AppData.CurrentUser.Id;//
            OutstoreOrder.InsertDate = DateTime.Now;//入库单 创建时间
                                                    //对象的Id 会有正确的值->操作的行数
            int count = outstoreOrderRepository.Insert(OutstoreOrder);
            //如果成功插入数据
            if (count > 0)
            {
                //新增入库明细(集合)
                //遍历
                foreach (var item in Outstores)
                {
                    //两个外键做判断
                    //物资
                    if (item.Cargo == null || item.Cargo.Id == 0)
                    {
                        continue;
                    }
                    else
                    {
                        item.CargoId = item.Cargo.Id;
                    }
                    //库位
                    if (item.Location == null || item.Location.Id == 0)
                    {
                        continue;
                    }
                    else
                    {
                        item.LocationId = item.Location.Id;
                    }
                    // Number 和 Price 有值，不做判断

                    // InstoreOrderId->count行数 
                    item.OutstoreOrderId = OutstoreOrder.Id;
                    item.InsertDate = DateTime.Now;
                    //插入-所有Instores : 一对多的子表数据
                    count += outstoreRepository.Insert(item);
                }
            }

            /// <summary>
            ///  数据库中有单号，
            ///  当前单号序列=原有序列+1
            /// </summary> 
            if (count > 1)
            {
                MessageBox.Show("出库成功");
                //入库明细清空，新入库
                Outstores = new ObservableCollection<Outstore>();
                OutstoreOrder = new OutstoreOrder();//新
                                                    //重新生成一个入库单号
                OutstoreOrder.NameEx = Guid.NewGuid().ToString();
            }

        }

        /// <summary>
        ///  添加一条入库明细
        /// </summary> 
        private void OnCreateInstoreCommand()
        {
            Outstores.Add(new Outstore());//new一个函数
        }

        private void OnWarehouseSelectionChangedCommand(Warehouse warehouse)
        {
            //  查库位--所属仓库变动
            Locations = locationRepository.GetAll().Where(t => t.WarehouseId == warehouse.Id).ToList();

        }

        private void OnLoadedCommand()
        {
            Outstores = new ObservableCollection<Outstore>();//初始化
            Load();// 基础数据  
            CreateOrder();// 单号 自动生成

        }
        private void Load()
        {
            // 所有的仓库查询 Warehouses
            // 所有的客户 Customer
            // 所有的库位 Locations
            Warehouses = warehouseRepository.GetAll();
            Customers = customerRepository.GetAll();
            // 所有的物资 Cargos
            Cargos = cargoRepository.GetAll();

        }

        // 入库单号 自动生成
        private void CreateOrder()
        {
            // Guid-全局唯一标识符
            OutstoreOrder.NameEx = Guid.NewGuid().ToString();
            // 扩展实体类
        }

    }
}
