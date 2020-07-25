import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { users } from './users.reducer';
import { alert } from './alert.reducer';
import { recipesReducer } from './recipe.reducer';

const rootReducer = combineReducers({
  authentication,
  users,
  alert,
  recipesReducer
});

export default rootReducer;