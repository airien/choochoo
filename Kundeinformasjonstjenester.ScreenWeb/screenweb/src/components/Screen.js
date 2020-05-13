import React, { useEffect, useState, Fragment } from "react";
import * as signalR from '@microsoft/signalr';

const Screen = () => {
 
 const [connection, setConnection] = useState("");
 const [hubConnection, setHubConnection] = useState(0);
 const [url, setUrl] = useState("");
 const [failed, setFailed] = useState(false);
 useEffect(() => {
    if (!connection && !failed && !hubConnection) {
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:44364/screen?stationId=1&trackId=1&screenId=123")
            .configureLogging(signalR.LogLevel.Information)
            .build();
        setHubConnection(hubConnection);
        hubConnection
        .start()
        .then(() => {
            setConnection(hubConnection.id);
            console.log('Connection started!');
        })
        .catch(err => {
            setFailed(true);
            console.log('Error while establishing connection :(')
        });
        hubConnection.on('ConnectToUrl', (url) => {
            setUrl(url);
        });
    }
  });
return(   <div>
    <div>
        <p>connection: {connection}</p>
        <p>url: {url}</p>
    </div>
            <img src={url}/>
        </div> );
}



export default Screen;
