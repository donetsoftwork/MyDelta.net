using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDeltaBench.Supports;

public class TestClass
{
    public int Id { get; set; }

    public string? Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public int IntField;
    public string StringField;
    public string StringProperty { get; set; }
    public DateTime DateTimeField;
}