using SmallProgramDemo.Core.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmallProgramDemo.Core.Abstract
{
    public abstract class QueryParameters : INotifyPropertyChanged
    {
        

        //默认的单页显示的数据条数
        private const int DefaultPageSize = 10;
        //默认的单页最多显示的数据条数
        private const int DefaultMaxPageSize = 100;

        //总共的页数属性
        private int _pageIndex;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value >= 0 ? value : 0;
        }

        //每页显示的数据量
        private int _pageSize = DefaultPageSize;
        public virtual int PageSize
        {
            get => _pageSize;
            set => SetField(ref _pageSize, value);
        }

        //每页最大显示的数据量
        private int _maxPageSize = DefaultMaxPageSize;
        protected internal virtual int MaxPageSize
        {
            get => _maxPageSize;
            set => SetField(ref _maxPageSize, value);
        }

        //排序属性
        private string _orderBy;
        public string OrderBy
        {
            get => _orderBy;
            set => _orderBy = value ?? nameof(IEntity.id);
        }
        public string Fields { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            if (propertyName == nameof(PageSize) || propertyName == nameof(MaxPageSize))
            {
                SetPageSize();
            }
            return true;
        }
        private void SetPageSize()
        {
            if (_maxPageSize <= 0)
            {
                _maxPageSize = DefaultMaxPageSize;
            }
            if (_pageSize <= 0)
            {
                _pageSize = DefaultPageSize;
            }
            _pageSize = _pageSize > _maxPageSize ? _maxPageSize : _pageSize;
        }
    }
}
