
# Magic Lambda Json

[![Build status](https://travis-ci.org/polterguy/magic.lambda.json.svg?master)](https://travis-ci.org/polterguy/magic.lambda.json)

This project provides JSON helper slots for [Magic](https://github.com/polterguy/magic). More specifically, it provides the following slots.

* __[json2lambda]__ - Creates a lambda object out of a JSON input string.
* __[lambda2json]__ - Creates JSON out of a lambda object.

## Usage

```
.json:@"{""property"": ""value""}"
json2lambda:x:-
lambda2json:x:-/*
```

The **[lambda2json]** slot can optionally take a **[format]** argument, with a boolean _"true"_ value, which implies the
resulting JSON will be indeneted and nicely formated, making it more readable.

## License

Although most of Magic's source code is publicly available, Magic is _not_ Open Source or Free Software.
You have to obtain a valid license key to install it in production, and I normally charge a fee for such a
key. You can [obtain a license key here](https://servergardens.com/buy/).
Notice, 7 days after you put Magic into production, it will stop functioning, unless you have a valid
license for it.

* [Get licensed](https://servergardens.com/buy/)
