
using ShoppingApp.Core;
using ShoppingApp.Types;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ShoppingItemList.ViewModels
{
    public class ApplicationViewModel : DependencyObject
    {
        public static readonly DependencyProperty ShopsProperty = 
            DependencyProperty.Register("Shops", typeof(IEnumerable<string>), typeof(ApplicationViewModel));

        public static readonly DependencyProperty SelectedShopProperty = 
            DependencyProperty.Register("SelectedShop", typeof(string), typeof(ApplicationViewModel), 
                new PropertyMetadata(SelectedShopChanged));

        public static readonly DependencyProperty ItemsInShopProperty =
            DependencyProperty.Register("ItemsInShop", typeof(IEnumerable<ShoppingItem>), typeof(ApplicationViewModel));

        public IEnumerable<string> Shops
        {
            get { return (IEnumerable<string>) GetValue(ShopsProperty); }
            private set { SetValue(ShopsProperty, value); }
        }

        public string SelectedShop
        {
            get { return (string)GetValue(SelectedShopProperty); }
            private set { SetValue(SelectedShopProperty, value); }
        }

        public IEnumerable<ShoppingItem> ItemsInShop
        {
            get { return (IEnumerable<ShoppingItem>)GetValue(ItemsInShopProperty); }
            private set { SetValue(ItemsInShopProperty, value); }
        }

        public static void SelectedShopChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ApplicationViewModel This = (ApplicationViewModel) obj;
            This.UpdateItemsList((string)args.NewValue);
            This.AddCommand.FireCanExecuteChanged();
        }

        private void UpdateItemsList(string shopName)
        {
            ItemsInShop = m_application.EnumItemsInShop(shopName);
        }

        private ShoppingItemListApplication m_application;

        public ApplicationViewModel()
        {
            m_application = new ShoppingItemListApplication();

            Shops = m_application.EnumShops();
        }

        #region Commands

        private ExitCommand m_exitCommand;

        public ExitCommand ExitCommand
        {
            get
            {
                if (null == m_exitCommand)
                    m_exitCommand = new ExitCommand();
                return m_exitCommand;
            }
        }

        private AddCommand m_addCommand;

        public AddCommand AddCommand
        {
            get
            {
                if (null == m_addCommand)
                    m_addCommand = new AddCommand(this);
                return m_addCommand;
            }
        }

        #endregion
    }

    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }

    public class AddCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ApplicationViewModel m_application;

        public void FireCanExecuteChanged()
        {
            if (null != CanExecuteChanged)
                CanExecuteChanged(this, new EventArgs());
        }

        public AddCommand(ApplicationViewModel application)
        {
            m_application = application;
        }

        public bool CanExecute(object parameter)
        {
            return m_application.SelectedShop != null;
        }

        public void Execute(object parameter)
        {
            
        }
    }
}