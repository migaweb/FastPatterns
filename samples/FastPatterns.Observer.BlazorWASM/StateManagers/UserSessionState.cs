namespace FastPatterns.Observer.BlazorWASM.StateManagers;

public class UserSessionState : Observer
{
  public string CurrentUser { get; private set; } = "Alice";

  public void SetUsername(string user)
  {
    CurrentUser = user;
    BroadcastStateChange();
  }
}
