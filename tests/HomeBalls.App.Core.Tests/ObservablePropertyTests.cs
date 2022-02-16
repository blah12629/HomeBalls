// namespace CEo.Pokemon.HomeBalls.App.Core.Tests;

// public class ObservablePropertyTests : ObservablePropertyTests<String?>
// {
//     public ObservablePropertyTests() : base(default) { }

//     public override void Value_ShouldNotRaisePropertyChanged_WhenSetWithSameValue() =>
//         Value_ShouldTestPropertyChanged_WhenSet(
//             "value1",
//             "value2",
//             (monitor, eventName) => monitor.Should().Raise(eventName));

//     public override void Value_ShouldRaisePropertyChangedWithNewValue() =>
//         Value_ShouldTestPropertyChanged_WhenSet(
//             "value1",
//             "value1",
//             (monitor, eventName) => monitor.Should().NotRaise(eventName));

//     public override void Value_ShouldRaisePropertyChangedWithOldValue() =>
//         Value_ShouldTestPropertyChanged_WhenSet(
//             "value1",
//             "value2",
//             (monitor, eventName) => monitor.Should().Raise(eventName),
//             (e, initialValue, newValue) => e.OldValue.Should().Be(initialValue));

//     public override void Value_ShouldRaisePropertyChanged_WhenSetWithDifferentValue() =>
//         Value_ShouldTestPropertyChanged_WhenSet(
//             "value1",
//             "value2",
//             (monitor, eventName) => monitor.Should().Raise(eventName),
//             (e, initialValue, newValue) => e.NewValue.Should().Be(newValue));
// }

// public abstract class ObservablePropertyTests<T>
// {
//     protected ObservablePropertyTests(
//         T defaultValue
//     )
//     {
//         EventRaiser = new EventRaiser();
//         Sut = new ObservableProperty<T>(defaultValue, nameof(Sut), EventRaiser);
//     }

//     protected IEventRaiser EventRaiser { get; }

//     protected ObservableProperty<T> Sut { get; }

//     [Fact]
//     public abstract void Value_ShouldRaisePropertyChanged_WhenSetWithDifferentValue();

//     [Fact]
//     public abstract void Value_ShouldNotRaisePropertyChanged_WhenSetWithSameValue();

//     [Fact]
//     public abstract void Value_ShouldRaisePropertyChangedWithOldValue();

//     [Fact]
//     public abstract void Value_ShouldRaisePropertyChangedWithNewValue();

//     protected virtual void Value_ShouldTestPropertyChanged_WhenSet(
//         T initialValue,
//         T newValue,
//         Action<IMonitor<ObservableProperty<T>>, String> raiseAssertions,
//         Action<HomeBallsPropertyChangedEventArgs, T, T>? eventAssertions = default)
//     {
//         Sut.Value = initialValue;
//         HomeBallsPropertyChangedEventArgs? eventArgs = default;
//         Sut.PropertyChanged += (sender, e) => eventArgs = e;
//         var monitor = Sut.Monitor();

//         Sut.Value = newValue;
//         raiseAssertions.Invoke(monitor, nameof(Sut.PropertyChanged));
//         eventAssertions?.Invoke(eventArgs!, initialValue, newValue);
//     }
// }