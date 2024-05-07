using Tokobaju.Entities;

namespace Tokobaju.Utils;

public interface IJwtUtil
{
    string GenerateToken(User payload);
}