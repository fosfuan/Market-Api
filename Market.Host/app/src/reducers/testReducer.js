import * as actionTypes from '../actions/actionTypes';
import initialState from './initialState';

export default function testReducer(state = initialState, action){
    switch (action.type){
        case actionTypes.CALL_TEST :
            return state;        
        default :
            return state;
    }
}