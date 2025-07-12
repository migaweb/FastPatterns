using FastPatterns.Observer.BlazorWASM.StateManagers;
using Microsoft.AspNetCore.Components;

namespace FastPatterns.Observer.BlazorWASM.Components;

public partial class UpdateUser : ComponentBase
{
  [Inject] UserSessionState UserSessionState { get; set; } = default!;

  private string Username { get; set; } = default!;

  protected override void OnInitialized()
  {
    base.OnInitialized();
    Username = UserSessionState.CurrentUser;
  }

  private void UpdateUserName()
  {
    if (!string.IsNullOrWhiteSpace(Username))
    {
      UserSessionState.SetUsername(Username);
    }
  }
}
