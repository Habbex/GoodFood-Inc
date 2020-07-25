import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import * as recipeActions  from "../_actions/recipe.actions";
import { useToasts } from "react-toast-notifications";
import { Link } from "react-router-dom";
import RecipeFrom from "../HomePage/RecipeForm.js";
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
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";

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

const RecipesList = ({ classes, ...props }) => {
  const [currentId, setCurrentId] = useState(0);

  useEffect(() => {
    props.fetchAllRecipes();
  }, []); //componentDidMount

  const { addToast } = useToasts();
  const onDelete = (id) => {
    if (window.confirm("Are you sure you want to delete this record?"))
      props.deleteRecipe(id, () =>
        addToast("Deleted successfully", { appearance: "info" })
      );
  };

  return (
    <Paper className={classes.paper}  elevation={3}>
        <Grid container>
          <Grid item sx={6}>
          <RecipeFrom {...{ currentId, setCurrentId }} />
          </Grid>
          <Grid item xs={6}>
            <TableContainer>
              <Table>
                <TableHead className={classes.root}>
                  <TableRow>
                    <TableCell>Title</TableCell>
                    <TableCell>Description</TableCell>
                    <TableCell>Category</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                {console.log(props.RecipeActionList)}
                {props.RecipeActionList.map((recipe, index) => {
                  return (
                    <TableRow key={index} hover onClick={()=>{setCurrentId(recipe.recipeId)}}>
                      <TableCell>{recipe.title}</TableCell>
                      <TableCell>{recipe.description}</TableCell>
                      <TableCell>{recipe.category}</TableCell>
                      <TableCell>
                        <ButtonGroup variant="text">
                          <Button>
                            <EditIcon
                              color="primary"
                              onClick={() => {
                                setCurrentId(recipe.id);
                              }}
                            />
                          </Button>
                          <Button>
                            <DeleteIcon
                              color="secondary"
                              onClick={() => onDelete(recipe.id)}
                            />
                          </Button>
                        </ButtonGroup>
                      </TableCell>
                    </TableRow>
                  );
                })}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>
        </Grid>
      </Paper>
  );
};


const mapStateToProps = (state) => ({
  RecipeActionList: state.recipesReducer.list,
});

const mapActionToProps = {
  fetchAllRecipes: recipeActions.fetchAllRecipes
};

export default connect(
  mapStateToProps,
  mapActionToProps
)(withStyles(styles)(RecipesList));
