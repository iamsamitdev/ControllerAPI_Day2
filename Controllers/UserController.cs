using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")] // api/User
public class UserController : ControllerBase
{

    // Mock data for users
    private static readonly List<User> _users = new List<User>
    {
        new User { 
            Id = 1,
            Username = "john",
            Email = "john@email.com",
            Fullname = "John Doe"
        },
        new User { 
            Id = 2,
            Username = "samit",
            Email = "samit@email.com",
            Fullname = "Samit Koyom"
        },
    };

    // Get all users
    // GET: api/User
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        // IEnumerable คืออะไร
        // IEnumerable เป็น interface ใน .NET Framework ที่ใช้แทน collection ของ object
        // interface นี้กำหนด method เพียงตัวเดียวคือ GetEnumerator()

        // วนซ้ำผ่าน collection โดยใช้ foreach
        // foreach (var user in _users)
        // {
        //     Console.WriteLine($"{user.Id} - {user.Username}");
        // }

        return Ok(_users);
    }

    // Get user by id
    // GET: api/User/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        // LINQ คืออะไร
        // Language-Integrated Query (LINQ) คือ ฟีเจอร์ใน .NET Framework ที่ใช้สำหรับ query ข้อมูลจาก collection ของ object
        // โดยใช้คำสั่งที่คล้ายกับ SQL แต่เขียนในรูปแบบของ C#
        var user = _users.Find(u => u.Id == id); // find user by 
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // Create new user
    // POST: api/User
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // Update user by id
    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user) {

        //Validate user id
        if (id != user.Id) {
            return BadRequest();
        }

        // Find existing user
        var existingUser = _users.Find(u => u.Id == id);
        if (existingUser == null) {
            return NotFound();
        }

        // Update user
        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.Fullname = user.Fullname;

        // Return updated user
        return Ok(existingUser);
    }

    // Delete user by id
    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        _users.Remove(user);
        return NoContent();
    }


}