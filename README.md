# HATEOAS - Asp.Net Core

[By spring.io](https://spring.io/understanding/HATEOAS) 

> HATEOAS (Hypermedia as the Engine of Application State) is a constraint of the REST application architecture.
 
> A hypermedia-driven site provides information to navigate the site's REST interfaces dynamically by including hypermedia links with the responses.

Nugets: https://www.nuget.org/profiles/aloji

## How to transform your API in a HATEOAS API?

HATEOASResult implements IActionResult, and his constructor receives two parameters, the object to be returned with links and one func to exceute the real IActionResult. 

For this reason it is very easy to transform your API in a HATEOAS API

```csharp

// GET api/values/5
[HttpGet("{id}", Name = RouteNames.Values_GetById)]
public IActionResult Get(int id)
{
    var value = this.values.FirstOrDefault(x => x.Id == id);
    if (value == null)
        return this.NotFound();

    return this.Ok(value);
}


// GET api/values/5
[HttpGet("{id}", Name = RouteNames.Values_GetById)]
public IActionResult Get(int id)
{
    var value = this.values.FirstOrDefault(x => x.Id == id);
    if (value == null)
        return this.NotFound();

    return this.HATEOASResult(value, (v) => this.Ok(v));
}

```

## Configuration

How to setup the models links?

Model:

```csharp

public class ValueResponse
{
    public int Id { get; set; }
    public string Value { get; set; }
}
   
```

Configure the relationship between the models and the links in Startup.cs

```csharp
   
public void ConfigureServices(IServiceCollection services)
{
    services.AddHATEOAS(options => 
    {
        options.AddLink<ValueResponse>("self",
          RouteNames.Values_GetById,
          HttpMethod.Get,
          (x) => new { id = x.Id });

        options.AddLink<ValueResponse>("all",
           RouteNames.Values_Get,
           HttpMethod.Get, null);

        options.AddLink<ValueResponse>("delete",
            RouteNames.Values_Delete,
            HttpMethod.Delete,
            (x) => new { id = x.Id });

        options.AddLink<ValueResponse>("update",
            RouteNames.Values_Update,
            HttpMethod.Put,
            (x) => new { id = x.Id });
    });

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}

```

Return a instance of HATEOASResult (IActionResult).

```csharp

// GET api/values
[HttpGet(Name = RouteNames.Values_Get)]
public IActionResult Get()
{
    return this.HATEOASResult(this.values, (v) => this.Ok(v));
}

```
The response generated is

```json

[{
  "Id": 1,
  "Value": "1",
  "_links": [
    {
      "rel": "self",
      "href": "http://localhost:52381/api/Values/1",
      "method": "GET"
    },
    {
      "rel": "all",
      "href": "http://localhost:52381/api/Values",
      "method": "DELETE"
    },
    {
      "rel": "delete",
      "href": "http://localhost:52381/api/Values/1",
      "method": "DELETE"
    },
    {
      "rel": "update",
      "href": "http://localhost:52381/api/Values/1",
      "method": "PUT"
    }]
  },
]

```
