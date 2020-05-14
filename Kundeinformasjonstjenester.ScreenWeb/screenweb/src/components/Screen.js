import React, { useEffect, useState, Fragment } from "react";
import * as signalR from "@microsoft/signalr";
import Iframe from "react-iframe";
import {useWindowDimensions} from '../hooks/WindowHooks'
const Screen = () => {
  const [connection, setConnection] = useState("");
  const [hubConnection, setHubConnection] = useState(0);
  const [error, setError] = useState("");
  const [url, setUrl] = useState("");
  const [failed, setFailed] = useState(false);
  const { height, width } = useWindowDimensions();
  useEffect(() => {
    if (!connection && !failed && !hubConnection) {
      const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(
          "https://localhost:44364/screen?stationId=1&trackId=1&screenId=AB"
        )
        .configureLogging(signalR.LogLevel.Information)
        .build();
      setHubConnection(hubConnection);
      hubConnection
        .start()
        .then(() => {
          setConnection(hubConnection.id);
          console.log("Connection started!");
        })
        .catch(err => {
          setFailed(true);
          setError(JSON.stringify(err));
          console.log("Error while establishing connection :(");
        });
      hubConnection.on("ConnectToUrl", url => {
        console.log("got new html: ", url);
        setUrl(url);
      });
    }
  });
  const renderError = () => {
    if (error) {
      return (
        <div>
          <p>connection: {connection}</p>
          <p style={{ color: "red" }}>error: {JSON.stringify(error)}</p>
        </div>
      );
    }
  };
 
  function renderContent() {
    if (!url || url.length === 0) return;

    return (
    <Iframe
      url={url}
      width={width}
      height={height}
      id="myId"
      display="initial"
      position="relative"
    />);
  }
  return (
    <div>
      {renderError()}
      {renderContent()}
    </div>
  );
};

export default Screen;
