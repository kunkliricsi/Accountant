package com.kunkliricsi.accountant.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.EditText;
import android.widget.TextView;

import com.kunkliricsi.accountant.R;
import com.kunkliricsi.accountant.database.DatabaseApi;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;

import java.util.Date;

public class AddShoppingListItemDialogFragment extends DialogFragment {

    private TextView Add;
    private TextView Cancel;
    private EditText Name;
    private EditText Comment;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_add_shopping_list_item, container, false);
        getDialog().getWindow().requestFeature(Window.FEATURE_NO_TITLE);

        Add = view.findViewById(R.id.shopping_add);
        Cancel = view.findViewById(R.id.shopping_cancel);
        Name = view.findViewById(R.id.shopping_name);
        Comment = view.findViewById(R.id.shopping_comment);

        Cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });

        Add.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (Name.getText().toString().trim().length() <= 0)
                    return;

                ShoppingListItem item = new ShoppingListItem();
                item.Name = Name.getText().toString().trim();
                item.DateOfCreation = new Date();

                String comment = Comment.getText().toString().trim();
                if (comment.length() > 0)
                item.Comment = comment;

                DatabaseApi.addShoppingListItem(item);
                dismiss();
            }
        });


        return view;
    }
}
