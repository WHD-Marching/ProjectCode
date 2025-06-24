using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;

namespace 仓储管理系统.ViewModels
{
    internal class CustomerViewModel : ObservableObject
    {
        private CustomerRepository CustomerRepository { get; } = new CustomerRepository();


        //当前的单个Cargo,没有带属性通知(所以继承集合类ObservableObject)
        public Customer customer = new Customer();
        public Customer Customer
        {
            get => customer;
            set { SetProperty(ref customer, value); }


        }
        //列表
        public List<Customer> customers;
        public List<Customer> Customers
        {
            get => customers;
            set { SetProperty(ref customers, value); }
        }

        public ICommand LoadedCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand AddOrUpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        //给到CustomerRepository
        public CustomerViewModel()
        {

            //Custormer = CargoRepository.GetAll();
            LoadedCommand = new RelayCommand(OnLoadedCommand);
            CreateCommand = new RelayCommand(OnCreateCommand);
            AddOrUpdateCommand = new RelayCommand(OnAddOrUpdateCommand);
            DeleteCommand = new RelayCommand<Customer>(OnDeleteCommand);
        }

        private void OnDeleteCommand(Customer customer)
        {
            if (customer == null || customer.Id == 0) return;
            //选项:是/否  No则返回
            if (MessageBox.Show("是否执行此操作?", "对话框", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            //删除 数据库删除指定的Cargo对象
            int count = CustomerRepository.Delete(customer);
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
            if (Customer == null)
            {
                MessageBox.Show("对象不能为空");
                return;
            }
            if (string.IsNullOrEmpty(Customer.Name))
            {
                MessageBox.Show("客户名称不能为空");
                return;
            }
            if (Customer.Id == 0)
            {
                //日期--本地时间
                Customer.InsertDate = DateTime.Now;

                int count = CustomerRepository.Insert(Customer);
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
                int count = CustomerRepository.Update(Customer);
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
            Customer = new Customer();
        }

        //插入,读数据库数据
        private void Load()
        {
            Customers = CustomerRepository.GetAll();
            
        }
    }
}

