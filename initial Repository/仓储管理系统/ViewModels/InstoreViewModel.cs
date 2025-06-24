using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Windows;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace 仓储管理系统.ViewModels
{
    internal class InstoreViewModel : ObservableObject
    {
        // 改为字段--增删改查类
        private InstoreOrderRepository instoreOrderRepository = new InstoreOrderRepository();
        private InstoreRepository instoreRepository = new InstoreRepository();
        private WarehouseRepository warehouseRepository = new WarehouseRepository();
        private SupplierRepository supplierRepository = new SupplierRepository();
        private LocationRepository locationRepository = new LocationRepository();
        private CargoRepository cargoRepository = new CargoRepository();


        #region 列表所有项，其他数据没有当前项
        // 当前入库单号
        private InstoreOrder instoreOrder = new InstoreOrder();
        public InstoreOrder InstoreOrder
        {
            get => instoreOrder;
            set => SetProperty(ref instoreOrder, value);
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

        // 供应商 
        //供应商
        private Supplier supplier = new Supplier();
        public Supplier Supplier
        {
            get => supplier;
            set { SetProperty(ref supplier, value); }
        }

        private List<Supplier> suppliers;
        public List<Supplier> Suppliers
        {
            get => suppliers;
            set { SetProperty(ref suppliers, value); }
        }


        //入库明细
        private Instore instore = new Instore();
        public Instore Instore
        {
            get => instore;
            set { SetProperty(ref instore, value); }
        }
        // 实体类--集合形式
        // 动态通知 
        //不需对 Instores 判断空
        private ObservableCollection<Instore> instores = new ObservableCollection<Instore>();
        public ObservableCollection<Instore> Instores
        {
            get => instores;
            set { SetProperty(ref instores, value); }
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

        public InstoreViewModel()
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
            Instores = new ObservableCollection<Instore>();
            InstoreOrder = new InstoreOrder();
            InstoreOrder.NameEx = Guid.NewGuid().ToString();
        }

        private void OnAddOrUpdateCommand()
        {
            // 入库表
            if (InstoreOrder == null) return;

            if (string.IsNullOrEmpty(InstoreOrder.Name))
            {
                MessageBox.Show("入库单号不能为空");
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
                InstoreOrder.WarehouseId = Warehouse.Id;
            }

            // 供应商
            if (Supplier == null || Supplier.Id == 0)
            {
                MessageBox.Show("请选择供应商");
                return;
            }
            else
            {
                InstoreOrder.SupplierId = Supplier.Id;
            }

            if (Instores == null || Instores.Count == 0)
            {
                MessageBox.Show("请增加入库的物资");
                return;
            }


            InstoreOrder.MemberId = AppData.CurrentUser.Id;//
            InstoreOrder.InsertDate = DateTime.Now;//入库单 创建时间
            //对象的Id 会有正确的值->操作的行数
            int count = instoreOrderRepository.Insert(InstoreOrder);
            //如果成功插入数据
            if (count > 0)
            {
                //新增入库明细(集合)
                //遍历
                foreach (var item in Instores)
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
                    item.InstoreOrderId = InstoreOrder.Id;
                    item.InsertDate = DateTime.Now;
                    //插入-所有Instores : 一对多的子表数据
                    count += instoreRepository.Insert(item);
                }
            }

            /// <summary>
            ///  数据库中有单号，
            ///  当前单号序列=原有序列+1
            /// </summary> 
            if (count > 1)
            {
                MessageBox.Show("入库成功");
                //入库明细清空，新入库
                Instores = new ObservableCollection<Instore>();
                InstoreOrder = new InstoreOrder();//新
                //重新生成一个入库单号
                InstoreOrder.NameEx = Guid.NewGuid().ToString();
            }

        }

        /// <summary>
        ///  添加一条入库明细
        /// </summary> 
        private void OnCreateInstoreCommand()
        {
            Instores.Add(new Instore());//new一个函数
        }

        private void OnWarehouseSelectionChangedCommand(Warehouse warehouse)
        {
            //  查库位--所属仓库变动
            Locations = locationRepository.GetAll().Where(t => t.WarehouseId == warehouse.Id).ToList();

        }

        private void OnLoadedCommand()
        {
            Instores = new ObservableCollection<Instore>();//初始化
            Load();// 基础数据  
            CreateOrder();// 单号 自动生成

        }
        private void Load()
        {
            // 所有的仓库查询 Warehouses
            // 所有的供应商 Suppliers
            // 所有的库位 Locations
            Warehouses = warehouseRepository.GetAll();
            Suppliers = supplierRepository.GetAll();
            // 所有的物资 Cargos
            Cargos = cargoRepository.GetAll();

        }

        // 入库单号 自动生成
        private void CreateOrder()
        {
            // Guid-全局唯一标识符
            InstoreOrder.NameEx = Guid.NewGuid().ToString();
            // 扩展实体类
        }

    }
}