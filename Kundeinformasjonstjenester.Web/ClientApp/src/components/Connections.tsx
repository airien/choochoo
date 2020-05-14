import React, {useEffect,Fragment,useState } from "react";
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
const useStyles = makeStyles({
    table: {
      minWidth: 300,
    },
  });
  
  const Information = () => {

    const classes = useStyles();
    const [connections, setConnections] = useState([]);
    const [loaded, setLoaded] = useState(false);
    useEffect(() => {
      const search = window.location.search;
      const params = new URLSearchParams(search);
      if(params && 
        (!loaded)) {
        
            fetch('https://localhost:44364/api/connection').then(function(response) {
                return response.json();
              }).then(function(data) {
                console.log("got response: ",data);
                setLoaded(true);
                setConnections(data);
              });
      }
    });

    return connections&& (
<TableContainer component={Paper}>
      <Table className={classes.table} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell align="right">Name</TableCell>
            <TableCell align="right">Timestamp</TableCell>
            <TableCell align="right">Origin</TableCell>
            <TableCell align="right">StationId</TableCell>
            <TableCell align="right">TrackId</TableCell>
            <TableCell align="right">ScreenId</TableCell>
            <TableCell align="right">Meta</TableCell>
            <TableCell align="right">Connected</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {connections.map((row:any) => (
            <TableRow key={row.connectionId}>
              <TableCell component="th" scope="row">
                {row.connectionId}
              </TableCell>
              <TableCell align="right">{row.name}</TableCell>
              <TableCell align="right">{row.timestamp}</TableCell>
              <TableCell align="right">{row.origin}</TableCell>
              <TableCell align="right">{row.stationId}</TableCell>
              <TableCell align="right">{row.trackId}</TableCell>
              <TableCell align="right">{row.screenId}</TableCell>
              <TableCell align="right">{row.meta}</TableCell>
              <TableCell align="right">{row.connected?"yes":"no"}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>

   
      ) || <p>informasjon ikke hentet..</p>;
  };
  
  export default Information;
