using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;

namespace 仓储管理系统.ViewModels
{
    internal class OutstoreOrderViewModel : ObservableObject
    {
        // 改为字段--增删改查类
        private OutstoreOrderRepository outstoreOrderRepository = new OutstoreOrderRepository();
        private OutstoreRepository outstoreRepository = new OutstoreRepository();
        //private WarehouseRepository warehouseRepository = new WarehouseRepository();
        //private SupplierRepository supplierRepository = new SupplierRepository();
        //private LocationRepository locationRepository = new LocationRepository();
        //private CargoRepository cargoRepository = new CargoRepository();


        //当前入库单号及集合
        private OutstoreOrder outstoreOrder = new OutstoreOrder();
        public OutstoreOrder OutstoreOrder
        {
            get => outstoreOrder;
            set => SetProperty(ref outstoreOrder, value);
        }
        //集合
        private List<OutstoreOrder> outstoreOrders = new List<OutstoreOrder>();
        public List<OutstoreOrder> OutstoreOrders
        {
            get => outstoreOrders;
            set => SetProperty(ref outstoreOrders, value);
        }

        /// <summary>
        /// 入库明细集合
        /// </summary>
        private List<Outstore> outstores = new List<Outstore>();
        public List<Outstore> Outstores
        {
            get => outstores;
            set => SetProperty(ref outstores, value);
        }


        public ICommand LoadedCommand { get; }
        public ICommand SelectionChangedCommand { get; }
        public OutstoreOrderViewModel()
        {
            LoadedCommand = new RelayCommand(OnLoadedCommand);
            //传递了参数InstoreOrder
            SelectionChangedCommand = new RelayCommand<OutstoreOrder>(OnSelectionChangedCommand);
        }

        //查入库明细集合-Instores
        private void OnSelectionChangedCommand(OutstoreOrder order)
        {
            if (order != null)
            {
                //具体数据--转列表
                Outstores = outstoreRepository.GetAll().Where(
                    t => t.OutstoreOrderId == order.Id).ToList();
            }
        }

        //查询所有
        private void OnLoadedCommand()
        {
            OutstoreOrders = outstoreOrderRepository.GetAll();
        }
    }
}
