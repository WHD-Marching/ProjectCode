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
    internal class SupplierViewModel : ObservableObject
    {
        private SupplierRepository SupplierRepository { get; } = new SupplierRepository();


        //当前的单个Cargo,没有带属性通知(所以继承集合类ObservableObject)
        public Supplier supplier = new Supplier();
        public Supplier Supplier
        {
            get => supplier;
            //引用传递: ref supplier 确保 SetProperty 能直接修改原字段，而非操作副本。
            set { SetProperty(ref supplier, value); }
        }
        //列表
        public List<Supplier> suppliers;
        public List<Supplier> Suppliers
        {
            get => suppliers;
            set { SetProperty(ref suppliers, value); }
        }

        public ICommand LoadedCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand AddOrUpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        //给到CustomerRepository
        public SupplierViewModel()
        {

            //Custormer = CargoRepository.GetAll();
            LoadedCommand = new RelayCommand(OnLoadedCommand);
            CreateCommand = new RelayCommand(OnCreateCommand);
            AddOrUpdateCommand = new RelayCommand(OnAddOrUpdateCommand);
            DeleteCommand = new RelayCommand<Supplier>(OnDeleteCommand);
        }

        private void OnDeleteCommand(Supplier supplier)
        {
            if (supplier == null || supplier.Id == 0) return;
            //选项:是/否  No则返回
            if (MessageBox.Show("是否执行此操作?", "对话框", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            //删除 数据库删除指定的Cargo对象
            int count = SupplierRepository.Delete(supplier);
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
            if (Supplier == null)
            {
                MessageBox.Show("对象不能为空");
                return;
            }
            if (string.IsNullOrEmpty(Supplier.Name))
            {
                MessageBox.Show("供应商名称不能为空");
                return;
            }
            if (Supplier.Id == 0)
            {
                //日期--本地时间
                Supplier.InsertDate = DateTime.Now;

                int count = SupplierRepository.Insert(Supplier);
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
                int count = SupplierRepository.Update(Supplier);
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
            Supplier = new Supplier();
        }

        //插入,读数据库数据
        private void Load()
        {
            Suppliers = SupplierRepository.GetAll();

        }
    }
}

