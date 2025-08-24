using task_management_system.Dto;
using task_management_system.Models;

namespace task_management_system.Mappers
{
    public static  class UserMapper
    {
        public static UserResponseDto ToUserDto(this User userModel)
        {
            return new UserResponseDto
            {
               Id = Guid.Parse(userModel.Id),
                
                UserName = userModel.UserName,

                Email = userModel.Email,

                Role = userModel.Role,

                CreatedAt = userModel.CreatedAt
            };
        }


        
    }
}
