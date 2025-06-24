using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using 仓储管理系统.Entities;
using 仓储管理系统.DAL;
using System.Windows.Input;

namespace 仓储管理系统.ViewModels
{
    internal class InstoreOrderViewModel : ObservableObject
    {
        // 改为字段--增删改查类
        private InstoreOrderRepository instoreOrderRepository = new InstoreOrderRepository();
        private InstoreRepository instoreRepository = new InstoreRepository();
        //private WarehouseRepository warehouseRepository = new WarehouseRepository();
        //private SupplierRepository supplierRepository = new SupplierRepository();
        //private LocationRepository locationRepository = new LocationRepository();
        //private CargoRepository cargoRepository = new CargoRepository();


        //当前入库单号及集合
        private InstoreOrder instoreOrder = new InstoreOrder();
        public InstoreOrder InstoreOrder
        {
            get => instoreOrder;
            set => SetProperty(ref instoreOrder, value);
        }
        //集合
        private List<InstoreOrder> instoreOrders = new List<InstoreOrder>();
        public List<InstoreOrder> InstoreOrders
        {
            get => instoreOrders;
            set => SetProperty(ref instoreOrders, value);
        }

        /// <summary>
        /// 入库明细集合
        /// </summary>
        private List<Instore> instores = new List<Instore>();
        public List<Instore> Instores
        {
            get => instores;
            set => SetProperty(ref instores, value);
        }


        public ICommand LoadedCommand { get; }
        public ICommand SelectionChangedCommand { get; }
        public InstoreOrderViewModel()
        {
            LoadedCommand = new RelayCommand(OnLoadedCommand);
            //传递了参数InstoreOrder
            SelectionChangedCommand = new RelayCommand<InstoreOrder>(OnSelectionChangedCommand);
        }

        //查入库明细集合-Instores
        private void OnSelectionChangedCommand(InstoreOrder order)
        {
            if (order != null)
            {
                //具体数据--转列表
                Instores = instoreRepository.GetAll().Where(
                    t => t.InstoreOrderId == order.Id).ToList();
            }
        }

        //查询所有
        private void OnLoadedCommand()
        {
            InstoreOrders = instoreOrderRepository.GetAll();
        }
    }
}
