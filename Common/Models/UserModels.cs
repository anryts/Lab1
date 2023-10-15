using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models;

public record UserCreateModel (string Name, string Email);
public record UserUpdateModel (Guid Id, string? Name, string? Email);
public record UserDeleteModel(Guid Id);


