using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using task_management_system.Interfaces;

namespace task_management_system.Hubs
{
    [Authorize]
    public class TasksHub: Hub<ITasksClientHub>
    {
        public override Task OnConnectedAsync() => base.OnConnectedAsync();
        public override Task OnDisconnectedAsync(Exception? ex) => base.OnDisconnectedAsync(ex);
    }
}
