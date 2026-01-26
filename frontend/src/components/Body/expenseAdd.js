import { SPENDO_APP_BASE_URL } from "../../config";

const ExpenseAdd = () => {
  const AddExpense = async () => {
    // const expense = {
    //   title: ,
    //   description: ,
    //   amount:
    // }

    const url = SPENDO_APP_BASE_URL + "/api/expense/add";
    try {
      const response = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({}),
      });
    } catch (error) {
      console.log(error.Message);
    }
  };

  return (
    <div id="expInp">
      <input type="text" name="expTitle" placeholder="Title" />
      <input type="text" name="expDesc" placeholder="Description" />
      <input type="number" name="expAmt" placeholder="Amount" />
      <button type="submit" onClick={AddExpense}>
        Submit
      </button>
    </div>
  );
};

export default ExpenseAdd;
