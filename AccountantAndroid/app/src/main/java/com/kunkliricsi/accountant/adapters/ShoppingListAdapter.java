package com.kunkliricsi.accountant.adapters;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.kunkliricsi.accountant.R;
import com.kunkliricsi.accountant.database.DatabaseApi;
import com.kunkliricsi.accountant.database.local.entities.Expense;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;

import org.w3c.dom.Text;

public class ShoppingListAdapter extends RecyclerView.Adapter<ShoppingListAdapter.ListItemHolder> {
    private ShoppingListItem[] items;

    @NonNull
    @Override
    public ListItemHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View itemView = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.list_shopping_item, viewGroup, false);

        return new ListItemHolder(itemView);
    }

    @Override
    public void onBindViewHolder(@NonNull ListItemHolder listItemHolder, int i) {
        ShoppingListItem item = items[i];
        listItemHolder.date.setText(item.DateOfCreation.toString());
        listItemHolder.name.setText(item.Name);
        Expense e = DatabaseApi.getExpense(item.ExpenseID);
        listItemHolder.expense.setText(e.DateOfPurchase.toString());
    }

    @Override
    public int getItemCount() {
        return items.length;
    }

    public static class ListItemHolder extends RecyclerView.ViewHolder {
        private TextView date;
        private TextView name;
        private TextView expense;


        public ListItemHolder(@NonNull View itemView) {

            super(itemView);
            date = itemView.findViewById(R.id.list_shopping_date);
            name = itemView.findViewById(R.id.list_shopping_name);
            expense = itemView.findViewById(R.id.list_shopping_expense);
        }
    }

    public ShoppingListAdapter() {
        items = DatabaseApi.getShoppingList();
    }
}
