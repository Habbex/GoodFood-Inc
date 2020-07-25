// import { recipeConstants } from "../_constants/recipe.constants";
import {ACTION_TYPES} from '../_actions/recipe.actions';

const initialState = {
  list: [],
};

export const recipesReducer = (state = initialState, action) => {
  switch (action.type) {
    case ACTION_TYPES.FETCH_ALL:
      return {
        ...state,
        list: [...action.payload],
      };
    default:
      return state;
  }
};
