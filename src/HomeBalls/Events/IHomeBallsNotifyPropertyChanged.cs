namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsNotifyPropertyChanged
{
    event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;
}