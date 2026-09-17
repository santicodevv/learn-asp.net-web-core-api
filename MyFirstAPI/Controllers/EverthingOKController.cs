using System;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EverthingOK
{
    [HttpGet]
    public string Get()
    {
        return "Hello, Everthing ok";
    }
}