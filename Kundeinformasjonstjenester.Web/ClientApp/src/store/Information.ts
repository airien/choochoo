import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface InformationState {
    isLoading: boolean;
    information: Information | null;
    requestedId: string | null;
}

export interface Information {
    id: string;
    src: string;
    summary: string;
    station: string;
    track: string;
    screen: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestInformationImageAction {
    type: 'REQUEST_INFORMATION';
    id: string;
}

interface ReceiveInformationAction {
    type: 'RECEIVE_INFORMATION';
    information: Information;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestInformationImageAction | ReceiveInformationAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestInformation: (id: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        console.log("request information:",id);
        const appState = getState();
        if (id && 

            (!appState || !appState.information || !appState.information.information
            || appState && 
            appState.information && 
            appState.information.information &&
            id !== appState.information.information.id)) {
            fetch(`api/information?id=${id}`)
                .then(response => response.json() as Promise<Information>)
                .then(data => {
                    console.log("fetched data",data);
                    dispatch({ type: 'RECEIVE_INFORMATION', information: data });
                });

            dispatch({ type: 'REQUEST_INFORMATION', id: id });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: InformationState = { information:null, requestedId: null, isLoading: false };

export const reducer: Reducer<InformationState> = (state: InformationState | undefined, incomingAction: Action): InformationState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_INFORMATION':
            return {
                requestedId: action.id,
                information: state.information,
                isLoading: true
            };
        case 'RECEIVE_INFORMATION':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.information.id === state.requestedId) {
                 return {
                    requestedId: null,
                    information: action.information,
                    isLoading: false
                };
            }
            break;
    }
           
    return state;
};
