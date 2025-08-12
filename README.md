# UserAgentProxy

This project is a very dumb proxy server that I wish I didn't have to write. It's very simple: it allows you to make HTTP calls and inject headers into the call to the upstream server. It spawned from my need to inject a user agent header in order to be able to trick certain URLs into thinking I was calling from a browser.

The API is as follows:

```
https://{your server}:{your port}/{request URL, encoded as base64}/{headers JSON dictionary, encoded as base 64}
```

For example, if you wanted to call `https://example.org` and provide the `User-Agent` header of `Mozilla/5.0 (Windows NT 10.0; Win64; x64)`, you'd construct the URL like this:

1. `https://example.org` to base64: `aHR0cHM6Ly9leGFtcGxlLm9yZw==`
2. `{"User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64)"}` to base64: `eyJVc2VyLUFnZW50IjogIk1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIn0=`
3. Resulting URL: `https://{your server}/aHR0cHM6Ly9leGFtcGxlLm9yZw==/eyJVc2VyLUFnZW50IjogIk1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIn0=`

Again, I wish I didn't have to make this, but I do. Do with it what you want. If you have some features you want added, make a PR or file an issue.

If you want to run this, I recommend using Docker. The latest version should be published on Github.