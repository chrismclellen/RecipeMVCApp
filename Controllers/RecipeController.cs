using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RecipeApp.Models;
using System.Diagnostics;

namespace RecipeApp.Controllers
{
    public class RecipeController : Controller
    {
        string connectionString = @"Data Source = (local)\SQLEXPRESS; Initial Catalog = MVCRecipes; Integrated Security = True";

        // GET: RecipeController
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblRecipes = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Recipes", sqlCon);
                sqlDa.Fill(dtblRecipes);
            }

            return View(dtblRecipes);
        }

        // GET: RecipeController/Details/5
        public ActionResult Details(int id)
        {
            RecipeModel recipeModel = new RecipeModel();
            DataTable dtblRecipe = new DataTable();
            DataTable dtblIngredients = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * from Recipes WHERE RecipeID = @RecipeID;";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@RecipeID", id);
                sqlDa.Fill(dtblRecipe);

                query = "SELECT * from Ingredients WHERE RecipeID = @RecipeID;";
                sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@RecipeID", id);
                sqlDa.Fill(dtblIngredients);
            }

            recipeModel.Ingredients = new List<IngredientModel>();
            for (int i = 0; i < dtblIngredients.Rows.Count; i++)
            {
                IngredientModel ingredientModel = new IngredientModel();
                ingredientModel.IngredientID = Convert.ToInt32(dtblIngredients.Rows[i][0].ToString());
                ingredientModel.RecipeID = Convert.ToInt32(dtblIngredients.Rows[i][1].ToString());
                ingredientModel.IngredientName = dtblIngredients.Rows[i][2].ToString();
                ingredientModel.Quantity = Convert.ToInt32(dtblIngredients.Rows[i][3].ToString());
                ingredientModel.UOM = dtblIngredients.Rows[i][4].ToString();
                recipeModel.Ingredients.Add(ingredientModel);

            }

            if (dtblRecipe.Rows.Count == 1)
            {
                recipeModel.RecipeID = Convert.ToInt32(dtblRecipe.Rows[0][0].ToString());
                recipeModel.RecipeName = dtblRecipe.Rows[0][1].ToString();
                recipeModel.RecipeInstructions = dtblRecipe.Rows[0][2].ToString();
                return View(recipeModel);
            }
            else
                return RedirectToAction("Index");
        }

        // GET: RecipeController/Create
        public ActionResult Create()
        {
            return View(new RecipeModel());
        }

        // POST: RecipeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeModel recipeModel)
        {
            int returnValue = -1;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Recipes VALUES(@RecipeName, @RecipeInstructions); SELECT SCOPE_IDENTITY();";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@RecipeName", recipeModel.RecipeName);
                sqlCmd.Parameters.AddWithValue("@RecipeInstructions", recipeModel.RecipeInstructions);
                object returnObj = sqlCmd.ExecuteScalar();

                if (returnObj != null)
                {
                    int.TryParse(returnObj.ToString(), out returnValue);
                }

                query = "INSERT INTO Ingredients VALUES(@RecipeID, @IngredientName, @Quantity, @UOM);";
                foreach (IngredientModel ingredient in recipeModel.Ingredients)
                {
                    sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@RecipeID", returnValue);
                    sqlCmd.Parameters.AddWithValue("@IngredientName", ingredient.IngredientName);
                    sqlCmd.Parameters.AddWithValue("@Quantity", ingredient.Quantity);
                    sqlCmd.Parameters.AddWithValue("@UOM", ingredient.UOM);
                    sqlCmd.ExecuteNonQuery();
                }
                

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: RecipeController/Edit/5
        public ActionResult Edit(int id)
        {
            RecipeModel recipeModel = new RecipeModel();
            DataTable dtblRecipe = new DataTable();
            DataTable dtblIngredients = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * from Recipes WHERE RecipeID = @RecipeID;";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@RecipeID", id);
                sqlDa.Fill(dtblRecipe);

                query = "SELECT * from Ingredients WHERE RecipeID = @RecipeID;";
                sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@RecipeID", id);
                sqlDa.Fill(dtblIngredients);
            }

            recipeModel.Ingredients = new List<IngredientModel>();
            for (int i = 0; i < dtblIngredients.Rows.Count; i++) 
            {
                IngredientModel ingredientModel = new IngredientModel();
                ingredientModel.IngredientID = Convert.ToInt32(dtblIngredients.Rows[i][0].ToString());
                ingredientModel.RecipeID = Convert.ToInt32(dtblIngredients.Rows[i][1].ToString());
                ingredientModel.IngredientName = dtblIngredients.Rows[i][2].ToString();
                ingredientModel.Quantity = Convert.ToInt32(dtblIngredients.Rows[i][3].ToString());
                ingredientModel.UOM = dtblIngredients.Rows[i][4].ToString();
                recipeModel.Ingredients.Add(ingredientModel);

            }

            if (dtblRecipe.Rows.Count == 1)
            {
                recipeModel.RecipeID = Convert.ToInt32(dtblRecipe.Rows[0][0].ToString());
                recipeModel.RecipeName = dtblRecipe.Rows[0][1].ToString();
                recipeModel.RecipeInstructions = dtblRecipe.Rows[0][2].ToString();
                return View(recipeModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: RecipeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecipeModel recipeModel)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Recipes SET RecipeName = @RecipeName, RecipeInstructions = @RecipeInstructions WHERE RecipeID = @RecipeID;";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@RecipeID", recipeModel.RecipeID);
                sqlCmd.Parameters.AddWithValue("@RecipeName", recipeModel.RecipeName);
                sqlCmd.Parameters.AddWithValue("@RecipeInstructions", recipeModel.RecipeInstructions);
                sqlCmd.ExecuteNonQuery();


                foreach (IngredientModel ingredient in recipeModel.Ingredients)
                {
                    if (ingredient.IngredientID > -1)
                    {
                        query = "UPDATE Ingredients SET IngredientName = @IngredientName, Quantity = @Quantity, UOM = @UOM Where IngredientID = @IngredientID;";
                        sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@IngredientID", ingredient.IngredientID);
                    }
                    else
                    {
                        query = "INSERT INTO Ingredients VALUES(@RecipeID, @IngredientName, @Quantity, @UOM);";
                        sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@RecipeID", recipeModel.RecipeID);
                    }

                    sqlCmd.Parameters.AddWithValue("@IngredientName", ingredient.IngredientName);
                    sqlCmd.Parameters.AddWithValue("@Quantity", ingredient.Quantity);
                    sqlCmd.Parameters.AddWithValue("@UOM", ingredient.UOM);
                    sqlCmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: RecipeController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Recipes Where RecipeID = @RecipeID; DELETE FROM Ingredients Where RecipeID = @RecipeID;";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@RecipeID", id);
                sqlCmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

    }
}
