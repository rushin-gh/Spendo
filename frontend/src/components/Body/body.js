import { useEffect, useState } from "react";
import { GetAllExpenses, DeleteExpense } from "../../utils/expenseFunctions";
import ExpenseAdd from "./expenseAdd";

const Body = () => {
  const [expenses, setExpenses] = useState([]);
  const [editingExpense, setEditingExpense] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    loadExpenses();
  }, []);

  const loadExpenses = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await GetAllExpenses();
      setExpenses(data || []);
    } catch (err) {
      console.error("Error loading expenses:", err);
      setError("Failed to load expenses");
      setExpenses([]);
    } finally {
      setLoading(false);
    }
  };

  const handleUpdate = (expense) => {
    setEditingExpense(expense);
  };

  const handleDelete = async (expenseId) => {
    if (!window.confirm("Are you sure you want to delete this expense?")) {
      return;
    }

    try {
      setEditingExpense(null);
      await DeleteExpense(expenseId);
      await loadExpenses();
    } catch (err) {
      console.error("Error deleting expense:", err);
      alert("Failed to delete expense. Please try again.");
    }
  };

  const renderContent = () => {
    if (loading) {
      return <div>Loading expenses...</div>;
    }

    if (error) {
      return <div>Error: {error}</div>;
    }

    if (expenses.length === 0) {
      return <div>No expenses found. Add your first expense above!</div>;
    }

    return (
      <table>
        <thead>
          <tr>
            <th>Title</th>
            <th>Description</th>
            <th>Amount</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {expenses.map((expense) => (
            <tr key={expense.id}>
              <td>{expense.title}</td>
              <td>{expense.description}</td>
              <td>{expense.amount}</td>
              <td>
                <button onClick={() => handleUpdate(expense)}>Edit</button>
                <button onClick={() => handleDelete(expense.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  };

  return (
    <div id="body">
      <ExpenseAdd
        editingExpense={editingExpense}
        setEditingExpense={setEditingExpense}
        loadExpenses={loadExpenses}
      />
      {renderContent()}
    </div>
  );
};

export default Body;
