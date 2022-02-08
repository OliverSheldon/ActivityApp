# ActivityApp
A small team status app using WebSockets
_________________________________________

Setup Instructions:

cd ActivityMonitor/ClientApp

npm install

npm run-script build

open sln in VS 2022

Run

_________________________________________

## Testing

UI doesn't actually do anything at the moment, but the solution can be tested in DevTools console to imitate what the UI would do:

const ws = new WebSocket("ws://"+location.host+"/ws");

ws.onmessage = event => {
  console.log(event);
};

ws.send("{\"Name\":\"test\"}");

## ToDo
Extend user class to add status enum

Connect UI to the WebSocket logic (currently in Home Component)

Make status updateable by UI and display all users

Push updates to all WebSocket clients

Use a more appropriate storage method for Users/Status
