package com.kunkliricsi.accountant.fragments;

import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v7.app.AlertDialog;

import com.kunkliricsi.accountant.R;
import com.kunkliricsi.accountant.database.DatabaseApi;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;

import java.util.ArrayList;

public class ShoppingListDialogFragment extends DialogFragment {
    public ArrayList<Integer> mSelectedItems;
    public ArrayList<ShoppingListItem> selectedItems;
    public String[] names;

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        mSelectedItems = new ArrayList<>();
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());

        builder.setTitle(R.string.select_shopping_list)
                .setMultiChoiceItems(names, null,
                        new DialogInterface.OnMultiChoiceClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog, int which,
                                                boolean isChecked) {
                                if (isChecked) {
                                    mSelectedItems.add(which);
                                } else if (mSelectedItems.contains(which)) {
                                    mSelectedItems.remove(Integer.valueOf(which));
                                }
                            }
                        })
                .setPositiveButton(R.string.ok, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        for (int i : mSelectedItems){
                            selectedItems.add(DatabaseApi.getShoppingListItemByName(names[i]));
                        }
                        dismiss();
                    }
                })
                .setNegativeButton(R.string.cancel, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        mSelectedItems.clear();
                        dismiss();
                    }
                });

        return builder.create();
    }
}
