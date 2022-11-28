using System;

namespace FrameworkDesign
{
    public class BindableProperty<T> where T : IEquatable<T>
    {

        public BindableProperty(T defaultValue = default)
        {
            _mValue = defaultValue;
        }

        private T _mValue = default(T);

        public T Value
        {
            get { return _mValue; }

            set
            {
                if(_mValue == null && value == null) return;

                if (_mValue == null)
                {
                    _mValue = value;
                    // 数值变化时触发注册器
                    _mOnValueChanged?.Invoke(value);
                    return;
                }

                if (value.Equals(_mValue)) return;
                
                _mValue = value;
                // 数值变化时触发注册器
                _mOnValueChanged?.Invoke(value);
            }
        }

        private Action<T> _mOnValueChanged = (v) => { };

        public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged)
        {
            _mOnValueChanged += onValueChanged;
            return new BindablePropertyUnRegister<T>
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public void UnRegisterOnValueChanged(Action<T> onValueChanged)
        {
            _mOnValueChanged -= onValueChanged;
        }
    }

    public class BindablePropertyUnRegister<T> : IUnRegister where T : IEquatable<T>
    {
        public BindableProperty<T> BindableProperty { get; set; }

        public Action<T> OnValueChanged { get; set; }

        public void UnRegister()
        {
            BindableProperty.UnRegisterOnValueChanged(OnValueChanged);

            BindableProperty = null;
            OnValueChanged = null;
        }
    }
}