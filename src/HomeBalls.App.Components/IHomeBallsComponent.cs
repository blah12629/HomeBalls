namespace CEo.Pokemon.HomeBalls.App.Components;

public interface IHomeBallsComponent :
    IComponent,
    IHandleEvent,
    IHandleAfterRender,
    IKeyed<Int32> { }
