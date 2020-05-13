import React, { useEffect, useState, Fragment } from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../store";
import * as StationInfoStore from "../store/StationInfo";
import { Station, Track, Screen } from "../store/StationInfo";
import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(theme => ({
  root: {
    flexGrow: 1
  },
  paper: {
    width: "100%",
    padding: 16
  },
  input: {width: 200}
}));

const InformationAdmin = () => {
  const stationState = useSelector((state: ApplicationState) => state.station);
  const state = useSelector((state: ApplicationState) => state);
  const stations = (stationState && stationState.stations
    ? stationState.stations
    : []) as Station[];
  const dispatch = useDispatch();
  const [availableTracks, setAvailableTracks] = useState([] as Track[]);
  const [availableScreens, setAvailableScreens] = useState([] as Screen[]);
  const [selectedStation, setSelectedStation] = useState({} as Station);
  const [selectedTrack, setSelectedTrack] = useState({} as Track);
  const [selectedScreen, setSelectedScreen] = useState({} as Screen);
  const [selectedUrl, setSelectedUrl] = useState("" as string);
  const classes = useStyles();

  useEffect(() => {
    if (!state.station || !state.station.isLoading) {
      dispatch(StationInfoStore.actionCreators.requestInformation());
    }
  });
  /*
  const onChange = (event: React.FormEvent<any>, params: ChangeEvent) => {
    setSelectedStation(params.newValue);
    console.log("selected station:",params.newValue);
  };
*/
  const changeStation = (v: Station) => {
    setSelectedStation(v);
    setAvailableTracks(v.tracks);
  };
  const changeTrack = (v: Track) => {
    setSelectedTrack(v);
    setAvailableScreens(v.screens);
  };
  const changeScreen = (v: Screen) => {
    setSelectedScreen(v);
  };

  return (
    <Fragment>
      <Grid container className={classes.root} spacing={2}>
        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <Grid container spacing={2}>
              <Grid item>
                <Autocomplete
                  id="stastion"
                  options={stations}
                  getOptionLabel={(option: Station) => option.name}
                  style={{ width: 300 }}
                  onChange={(e: object, v: any) => changeStation(v)}
                  renderInput={params => (
                    <TextField {...params} label="Stasjon" variant="outlined" />
                  )}
                />
              </Grid>
              <Grid item>
                <Autocomplete
                  id="track"
                  options={availableTracks}
                  getOptionLabel={(option: Track) => option.name}
                  style={{ width: 300 }}
                  onChange={(e: object, v: any) => changeTrack(v)}
                  renderInput={params => (
                    <TextField {...params} label="Spor" variant="outlined" />
                  )}
                />
              </Grid>
              <Grid item>
                <Autocomplete
                  id="screen"
                  options={availableScreens}
                  getOptionLabel={(option: Screen) => option.name}
                  style={{ width: 300 }}
                  onChange={(e: object, v: any) => changeScreen(v)}
                  renderInput={params => (
                    <TextField {...params} label="Skjerm" variant="outlined" />
                  )}
                />
              </Grid>
              <Grid item>
                <TextField  
                  style={{ width: 300 }}
                  id="bilde"
                  label="Bildeurl"
                  defaultValue=""
                  helperText="BildeUrl"
                  variant="outlined"
                  onChange={(value) => setSelectedUrl(value.target.value)}
                />
              </Grid>
            </Grid>
          </Paper>
        </Grid>

        <Grid item xs={12}></Grid>
        <Paper className={classes.paper}>
          <div>
            <h1>Du har valgt</h1>
            <p>stasjon: {selectedStation.name}</p>
            <p>spor: {selectedTrack.name}</p>
            <p>skjerm: {selectedScreen.name}</p>
            <p>url: {selectedUrl}</p>
          </div>
        </Paper>
        <Grid item>
          <Button variant="contained" color="primary">
            Send
          </Button>
        </Grid>
      </Grid>
    </Fragment>
  );
};

export default InformationAdmin;
