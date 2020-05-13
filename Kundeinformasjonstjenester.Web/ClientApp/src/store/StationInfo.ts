import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface StationState {
    isLoading: boolean;
    stations: Station[] | null;
}

export interface Station {
    id: string;
    name: string;
    tracks: Track[];
}
export interface Track {
    id: string;
    name: string;
    screens: Screen[];
}

export interface Screen {
    id: string;
    name: string;
}


// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestStationsAction {
    type: 'REQUEST_STATIONS';
}

interface ReceiveStationsAction {
    type: 'RECEIVE_STATIONS';
    stations: Station[]
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestStationsAction | ReceiveStationsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestInformation: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        console.log("request stations");
        const appState = getState();
        if (!appState || !appState.station || !appState.station.stations) {
            fetch(`api/stations`)
                .then(response => response.json() as Promise<Station[]>)
                .then(data => {
                    console.log("fetched data",data);
                    dispatch({ type: 'RECEIVE_STATIONS', stations: data });
                });

            dispatch({ type: 'REQUEST_STATIONS' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: StationState = { stations:null,  isLoading: false };

export const reducer: Reducer<StationState> = (state: StationState | undefined, incomingAction: Action): StationState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_STATIONS':
            return {
                stations: state.stations,
                isLoading: true
            };
        case 'RECEIVE_STATIONS':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.stations) {
                 return {
                    stations: action.stations,
                    isLoading: false
                };
            }
            break;
    }
           
    return state;
};
