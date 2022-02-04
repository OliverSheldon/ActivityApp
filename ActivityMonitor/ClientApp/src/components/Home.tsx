import * as React from 'react';
import { connect } from 'react-redux';
import Login from './Login';
import User from './User';

//ToDo: ws.send on login
//      send status updates

const Home = () => (
  <div>
    <Login/>    
  </div>
);

const ws = new WebSocket("ws://"+location.host+"/ws");

ws.onopen = event => {
  console.log("Connected");
  var test = JSON.stringify(new User("Oliver"));
  ws.send(test);
};

ws.onmessage = event => {
  console.log(event);
};

ws.onclose = event => {
  console.log("Disconnected");
};

export default connect()(Home);
