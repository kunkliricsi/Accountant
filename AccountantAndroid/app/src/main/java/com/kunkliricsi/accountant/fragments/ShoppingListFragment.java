package com.kunkliricsi.accountant.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.Fragment;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;

import com.kunkliricsi.accountant.R;
import com.kunkliricsi.accountant.adapters.ShoppingListAdapter;

public class ShoppingListFragment extends Fragment {

    private RecyclerView rview;
    private RecyclerView.Adapter adapter;
    private RecyclerView.LayoutManager manager;


    public static ShoppingListFragment newInstance() {
        return new ShoppingListFragment();
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_shopping_list, container, false);

        rview = view.findViewById(R.id.shopping_view);
        rview.setLayoutManager(new LinearLayoutManager(getActivity()));
        rview.setItemAnimator(new DefaultItemAnimator());
        rview.setAdapter(new ShoppingListAdapter());

        return view;
    }
}
