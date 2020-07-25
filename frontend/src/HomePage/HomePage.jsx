import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import RecipeList from './RecipesList';
import {
  Grid,
  Paper,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  withStyles,
  ButtonGroup,
  Button,
} from "@material-ui/core";
const styles = (theme) => ({
  root: {
    "& .MuiTableCell-head": {
      fontSize: "1.25rem",
    },
  },
  paper: {
    margin: theme.spacing(2),
    padding: theme.spacing(2),
  },
});


class HomePage extends React.Component {

  render() {
    const { classes } = this.props;
    return (
      <Paper className={classes.paper} elevation={3}>
        <Grid container>
          <RecipeList/>
            <p>
              <Link to="/login">Logout</Link>
            </p>
          </Grid>
      </Paper>
    );
  }
}

function mapStateToProps(state) {
  const { users, authentication } = state;
  const { user } = authentication;
  return {
    user,
    users
  };
}

const connectedHomePage = connect(mapStateToProps)(
  withStyles(styles)(HomePage)
);
export { connectedHomePage as HomePage };
