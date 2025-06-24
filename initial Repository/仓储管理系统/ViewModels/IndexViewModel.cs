using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;

namespace 仓储管理系统.ViewModels
{
    internal class IndexViewModel : ObservableObject
    {
        //实例-仓储实现
        // 使用原因--WPF的绑定系统只能识别属性，无法直接绑定到字段
        // 1. 需要将Repository暴露给XAML绑定;
        // 2. 当类可能被继承且需要允许子类重写Repository实现时
        public CargoRepository CargoRepository { get; } = new CargoRepository();
        public CustomerRepository CustomerRepository { get; } = new CustomerRepository();
        public InstoreRepository InstoreRepository { get; } = new InstoreRepository();
        public OutstoreRepository outstoreRepository { get; } = new OutstoreRepository();
        public SupplierRepository supplierRepository { get; } = new SupplierRepository();
        public WarehouseRepository warehouseRepository { get; } = new WarehouseRepository();
        public LocationRepository locationRepository { get; } = new LocationRepository();



        //物资数量
        private int cargoCount = 0;
        public int CargoCount
        {
            get => cargoCount;
            set => SetProperty(ref cargoCount, value);
        }
        //客户统计
        private int customerCount = 0;
        public int CustomerCount
        {
            get => customerCount;
            set => SetProperty(ref customerCount, value);
        }
        //入库明细
        private int instoreCount = 0;
        public int InstoreCount
        {
            get => instoreCount;
            set => SetProperty(ref instoreCount, value);
        }
        //出库明细
        private int outstoreCount = 0;
        public int OutstoreCount
        {
            get => outstoreCount;
            set => SetProperty(ref outstoreCount, value);
        }
        //供应商
        private int supplierCount = 0;
        public int SupplierCount
        {
            get => supplierCount;
            set => SetProperty(ref supplierCount, value);
        }
        //仓库(个)
        private int warehouseCount = 0;
        public int WarehouseCount
        {
            get => warehouseCount;
            set => SetProperty(ref warehouseCount, value);
        }
        //库位(个)
        private int locationCount = 0;
        public int LocationCount
        {
            get => locationCount;
            set => SetProperty(ref locationCount, value);
        }


        /// <summary>
        ///    命令
        /// </summary>
        public ICommand LoadedCommand { get; }
        public IndexViewModel()
        {
            CargoCount = 999;//测试
            LoadedCommand = new RelayCommand(OnLoadedCommand);
        }
        //加载
        private void OnLoadedCommand()
        {
            //<,Cargo> 序列中的元素数
            CargoCount = CargoRepository.GetAll().Count();
            CustomerCount = CustomerRepository.GetAll().Count();
            InstoreCount = InstoreRepository.GetAll().Count(); ;
            OutstoreCount = outstoreRepository.GetAll().Count(); ;
            SupplierCount = supplierRepository.GetAll().Count();
            WarehouseCount = warehouseRepository.GetAll().Count(); 
            LocationCount = locationRepository.GetAll().Count();
        }
    }
}
