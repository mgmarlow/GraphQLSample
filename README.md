## Sending Requests

### Web API Approach
Post requests to `localhost:5000/api/books`. Within the body of the request, provide a string of the form:
```
`query {
  books {
    isbn,
    name
  }
}`
```

### Middleware Approach

1. Configure middleware
```
public void Configure(IApplicationBuilder...)
{
    //...
    app.UseGraphQL();
    //...
}
```

2. Send a request in the following form to `localhost:5000/graph`:
```
query {
  books {
    isbn,
    name
  }
}
```


## Reference
* [Exploring GraphQL](http://asp.net-hacker.rocks/2017/05/29/graphql-and-aspnetcore.html)
