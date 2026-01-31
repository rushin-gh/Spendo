import { useEffect, useState } from "react";
import { GetAllExpenses, DeleteExpense } from "../../utils/expenseFunctions";
import ExpenseAdd from "./expenseAdd";

const Body = () => {
  const [expenses, setExpenses] = useState([]);
  const [editingExpense, setEditingExpense] = useState(null);

  useEffect(() => {
    loadExpenses();
  }, []);

  const loadExpenses = async () => {
    try {
      const expenses = await GetAllExpenses();
      setExpenses(expenses);
    } catch (err) {
      console.log(err.message);
      // Some error logging functionality
    }
  };

  const handleActions = async (event, obj) => {
    if (event == "UpdateExpenseBtnClicked") {
      await setEditingExpense(obj.exp);
    } else if (event == "DeleteExpenseBtnClicked") {
      await DeleteExpense(obj.id);
      loadExpenses();
    }
  };

  return (
    <>
      <div id="body">
        <ExpenseAdd
          editingExpense={editingExpense}
          setEditingExpense={setEditingExpense}
          loadExpenses={loadExpenses}
        />
        {!expenses ? (
          <div id="body">Error while loading expenses</div>
        ) : expenses.length == 0 ? (
          <div id="body">No expenses</div>
        ) : (
          <table>
            <thead>
              <tr>
                {/* <th>Sr</th> */}
                <th>Title</th>
                <th>Desc</th>
                <th>Amount</th>
                <th>Update</th>
                <th>Delete</th>
              </tr>
            </thead>
            <tbody>
              {expenses.map((exp) => {
                return (
                  <tr key={exp.id}>
                    {/* <td>{exp.id}</td> */}
                    <td>{exp.title}</td>
                    <td>{exp.description}</td>
                    <td>{exp.amount}</td>
                    <td
                      onClick={() =>
                        handleActions("UpdateExpenseBtnClicked", { exp: exp })
                      }
                    >
                      U
                    </td>
                    <td
                      onClick={() =>
                        handleActions("DeleteExpenseBtnClicked", { id: exp.id })
                      }
                    >
                      D
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
};

export default Body;
