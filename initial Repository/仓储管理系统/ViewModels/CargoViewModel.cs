using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebSockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;

namespace 仓储管理系统.ViewModels
{
    internal class CargoViewModel : ObservableObject
    {
        private CargoRepository CargoRepository { get; } = new CargoRepository();

        
        //当前的单个Cargo,没有带属性通知(所以继承集合类ObservableObject)
        public Cargo cargo = new Cargo();

        public Cargo Cargo
        {
            get => cargo;
            set { SetProperty(ref cargo, value); }


        }
        //列表
        public List<Cargo> cargos;
        public List<Cargo> Cargos
        {
            get => cargos;
            set { SetProperty(ref cargos, value); }
        }

        public ICommand LoadedCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand AddOrUpdateCommand { get; }
        public ICommand DeleteCommand { get;  }

        //给到CargoRepository
        public CargoViewModel()
        {

            //Cargos = CargoRepository.GetAll();
            LoadedCommand = new RelayCommand(OnLoadedCommand);
            CreateCommand = new RelayCommand(OnCreateCommand);
            AddOrUpdateCommand = new RelayCommand(OnAddOrUpdateCommand);
            DeleteCommand = new RelayCommand<Cargo>(OnDeleteCommand);
        }

        private void OnDeleteCommand(Cargo cargo)
        {
            if (cargo == null || cargo.Id == 0) return;
            //选项:是/否  No则返回
            if (MessageBox.Show("是否执行此操作?", "对话框", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            //删除 数据库删除指定的Cargo对象
            int count = CargoRepository.Delete(cargo);
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
            if (Cargo == null)
            {
                MessageBox.Show("物资对象不能为空");
                return;
            }
            if (string.IsNullOrEmpty(Cargo.Name))
            {
                MessageBox.Show("物资名称不能为空");
                return;
            }
            if (Cargo.Id == 0)
            {
                //日期--本地时间
                Cargo.InsertDate = DateTime.Now;

                int count = CargoRepository.Insert(Cargo);
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
                int count = CargoRepository.Update(Cargo);
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
            Cargo = new Cargo();
        }

        //插入,读数据库数据
        private void Load()
        {
            Cargos = CargoRepository.GetAll();
            //return;
        }
    }
}
