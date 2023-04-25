using Cinema.Exceptions;
using Cinema.Models;

namespace Cinema.Services;

public interface IUserService
{
    public List<ProfileDto> GetUsers();
    public Task PutUser(ProfileDto model);
}

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ProfileDto> GetUsers()
    {
        // return _context.Users.Select(x => new ProfileDto(x)).ToList();
        var users = _context.Users.ToList();
        var profiles = users.Select(user => new ProfileDto(user)).ToList();
        return profiles;
    }

    public async Task PutUser(ProfileDto model)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == model.Id);
        if (user == null) throw new UserNotFoundException();

        user.Id = model.Id;
        user.Email = model.Email;
        user.Name = model.Name;
        user.BirthDate = model.BirthDate;
        user.Gender = model.Gender;
        user.AvatarLink = model.AvatarLink;
        user.NickName = model.NickName;


        await _context.SaveChangesAsync();
    }
}