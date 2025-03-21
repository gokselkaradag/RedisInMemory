using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;

namespace RedisExchangeAPI.Web.Controllers;

public class HashTypeController : BaseController
{
    public string hashKey { get; set; } = "HashKey";
    public HashTypeController(RedisService redisService) : base(redisService)
    {
    }

    public IActionResult Index()
    {
        Dictionary<string,string> list = new Dictionary<string, string>();
        db.HashGetAll(hashKey).ToList().ForEach(x => list.Add(x.Name, x.Value));
        return View(list);
    }
    
    [HttpPost]
    public IActionResult Add(string name, string value)
    {
        db.HashSet(hashKey, name, value);
        return RedirectToAction("Index");
    }
    
    public IActionResult DeleteItem(string name)
    {
        db.HashDeleteAsync(hashKey, name);
        return RedirectToAction("Index");
    }
}