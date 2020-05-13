import React, {useEffect,Fragment } from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from '../store';
import * as InformationStore from '../store/Information';

  
  const Information = () => {
    const informationState = useSelector((state : ApplicationState)=> state.information);
    const state = useSelector((state : ApplicationState)=> state);
    const information = informationState ? informationState.information : null;
    const dispatch = useDispatch();
    useEffect(() => {
      const search = window.location.search;
      const params = new URLSearchParams(search);
      if(params && 
        (!state.information  || !state.information.isLoading)) {
        
        const id = params.get('id') || "";
        dispatch(InformationStore.actionCreators.requestInformation(id));
      }
    });

    return informationState && information && (
      <Fragment>
        <div>
          <p> Viser informasjon for</p>
          <p> Stasjon: {information.station}</p>
          <p> Spor:  {information.track}</p>
          <p> Skjerm:  {information.screen}</p>
        </div>
        <img src={information.src}/>
      </Fragment>

   
      ) || <p>informasjon ikke hentet</p>;
  };
  
  export default Information;
