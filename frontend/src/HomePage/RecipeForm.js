import React, { useState, useEffect } from "react";
import { Grid, TextField, withStyles, Button } from "@material-ui/core";
import { connect } from "react-redux";
import useRecipeForm from "./use.Recipe.Forms";
import { recipeActions } from "../_actions/recipe.actions";
import { useToasts } from "react-toast-notifications";

const styles = (theme) => ({
    root: {
      "& .MuiTextField-root": {
        margin: theme.spacing(1),
        minWidth: 230,
      },
    },
    smMargin: {
      margin: theme.spacing(1),
    },
  });
  
  const initialFieldValues = {
    title: "",
    description: "",
    category: "",
  };
  
  const RecipeForm = ({ classes, ...props }) => {
    //toast msg
    const {addToast}=useToasts();
    
    const validate = (fieldValues = values) => {
      let tempInput = { ...errors };
  
      if ("title" in fieldValues)
        tempInput.title = fieldValues.title ? "" : "This field is required.";
  
      if ("description" in fieldValues)
        tempInput.description = fieldValues.description ? "" : "This field is required.";
  
      if ("category" in fieldValues)
        tempInput.category = fieldValues.category
          ? ""
          : "This field is required.";
  
      setErrors({
        ...tempInput,
      });
      if (fieldValues == values) {
        return Object.values(tempInput).every((x) => x == "");
      }
    };
  
    const {
      values,
      setValues,
      errors,
      setErrors,
      handleInputChange,
      resetForm,
    } = useRecipeForm(initialFieldValues, validate, props.setCurrentId);
  
    // const handleSubmit = (e) => {
    //   e.preventDefault();
  
    //   if (validate()) {
    //     const onSuccessCreate = () => {
    //       resetForm();
    //       addToast("Created successfully", { appearance: "success" });
    //     };
    //     const onSuccessUpdate = () => {
    //       resetForm();
    //       addToast("Updated successfully", { appearance: "success" });
    //     };
  
    //     if (props.currentId == 0) props.createDCommands(values, onSuccessCreate);
  
    //     else props.updateDCommands(props.currentId, values, onSuccessUpdate);
    //   }
    // };
  
    useEffect(() => {
      if (props.currentId != 0)
        setValues({
          ...props.RecipeActionList.find((x) => x.id == props.currentId),
        });
    }, [props.currentId]);
    return (
      <form
        autoComplete="off"
        noValidate
        className={classes.root}
        // onSubmit={handleSubmit}
      >
        <Grid container>
          <Grid item xs={6}>
            <TextField
              name="title"
              variant="outlined"
              label="Title"
              value={values.title}
              // onChange={handleInputChange}
              // {...(errors.howTo && { error: true, helperText: errors.howTo })}
            />
            <TextField
              name="description"
              variant="outlined"
              label="Description"
              value={values.description}
              // onChange={handleInputChange}
              // {...(errors.line && { error: true, helperText: errors.line })}
            />
  
            <TextField
              name="category"
              variant="outlined"
              label="Category"
              value={values.category}
              // onChange={handleInputChange}
              // {...(errors.platform && {
              //   error: true,
              //   helperText: errors.platform,
              // })}
            />
               <div>
              <Button
                variant="contained"
                color="primary"
                type="submit"
                className={classes.smMargin}
              >
                Submit
              </Button>
              <Button
                variant="contained"
                className={classes.smMargin}
                // onClick={resetForm}
              >
                Reset
              </Button>
            </div>
          </Grid>
        </Grid>
      </form>
    );
  };
  
  const mapStateToProps = (state) => ({
    RecipeActionList: state.recipesReducer.list,
  });
  
  // const mapActionToProps = {
  //   createDCommands: recipeActions.createCommand,
  //   updateDCommands: recipeActions.updateCommand,
  // };
  
  export default connect(
    mapStateToProps,
    // mapActionToProps
  )(withStyles(styles)(RecipeForm));
  