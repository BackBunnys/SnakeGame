using System;

namespace SnakeGame.GUI
{
    class ValueBinding<T>
    {
        public Func<T> ValueSupplier { get; }
        public Action<T> OnValueChange { get; }

        public T Value { get => ValueSupplier.Invoke(); set => OnValueChange(value); }

        public ValueBinding(Func<T> valueSupplier, Action<T> onValueChange)
        {
            ValueSupplier = valueSupplier;
            OnValueChange = onValueChange;
        }
    }
}
