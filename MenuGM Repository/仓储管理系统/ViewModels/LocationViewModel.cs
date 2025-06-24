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

namespace 仓储管理系统.ViewModels
{
    internal class LocationViewModel : ObservableObject
    {
        private LocationRepository LocationRepository { get; } = new LocationRepository();
        private WarehouseRepository WarehouseRepository { get; } = new WarehouseRepository();

        //当前 
        public Location location = new Location();
        public Location Location
        {
            get => location;
            //引用传递: ref supplier 确保 SetProperty 能直接修改原字段，而非操作副本。
            set { SetProperty(ref location, value); }
        }
        //列表--所有
        public List<Location> locations;
        public List<Location> Locations
        {
            get => locations;
            set { SetProperty(ref locations, value); }
        }

        /// <summary>
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



        public ICommand LoadedCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand AddOrUpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        //给到CustomerRepository
        public LocationViewModel()
        {

            // .GetAll();
            LoadedCommand = new RelayCommand(OnLoadedCommand);
            CreateCommand = new RelayCommand(OnCreateCommand);
            AddOrUpdateCommand = new RelayCommand(OnAddOrUpdateCommand);
            DeleteCommand = new RelayCommand<Location>(OnDeleteCommand);
        }

        private void OnDeleteCommand(Location location)
        {
            if (location == null || location.Id == 0) return;
            //选项:是/否  No则返回
            if (MessageBox.Show("是否执行此操作?", "对话框", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            //删除 数据库删除指定的Cargo对象
            int count = LocationRepository.Delete(location);
            if (count > 0)
            {
                MessageBox.Show("操作成功");
                Load();
            }
            else
            {
                MessageBox.Show("操作失败");
                //return;
            }
        }
       
        private void OnAddOrUpdateCommand()
        {
            //前端已经绑定,不需要传参
            if (Location == null)
            {
                MessageBox.Show("对象不能为空");
                return;
            }
            if (string.IsNullOrEmpty(Location.Name))
            {
                MessageBox.Show("库位名称不能为空");
                return;
            }
            // 关于WarehouseName
            // 当前 Warehouse未选中/0
            if (Warehouse == null || Warehouse.Id == 0)
            {
                MessageBox.Show("请选择仓库");
                return;
            }
            else
            {
                //库位id=当前仓库id
                Location.WarehouseId = Warehouse.Id;
            }

            if (Location.Id == 0)
            {
                //日期--本地时间
                Location.InsertDate = DateTime.Now;

                int count = LocationRepository.Insert(Location);
                if (count > 0)
                {
                    MessageBox.Show("操作成功");
                    Load();
                }
                else
                {
                    MessageBox.Show("操作失败");
                }
            }
            else
            {
                //修改
                int count = LocationRepository.Update(Location);
                if (count > 0)
                {
                    MessageBox.Show("操作成功");
                }
                else
                {
                    MessageBox.Show("操作失败");
                }
            }
        }

        private void OnLoadedCommand()
        {
            //每次都会查询
            Load();
        }

        private void OnCreateCommand()
        {
            //=创建时
            Location = new Location();
        }

        //插入,读数据库数据
        private void Load()
        {
            Locations = LocationRepository.GetAll();

            // 对Combobox列表控件
            Warehouses = WarehouseRepository.GetAll();
            //可选择默认(第一个仓库) = Warehouses.FirstOrDefault();
        }
    }
}

