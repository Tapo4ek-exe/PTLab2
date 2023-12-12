import React from "react";
import {
  Body1Strong,
  Card,
  DataGrid,
  DataGridBody,
  DataGridCell,
  DataGridHeader,
  DataGridHeaderCell,
  DataGridRow,
  TableColumnDefinition,
  createTableColumn,
} from "@fluentui/react-components";

import { Product } from "../../models/Product";
import { Purchase } from "../Purchase/Purchase";

type ProductsProps = {
  products: Product[];
  onPurchaseCreated?: () => void;
  onError?: (error: string) => void;
};

const Products: React.FunctionComponent<ProductsProps> = ({
  products,
  onPurchaseCreated,
  onError,
}) => {
  const columns: TableColumnDefinition<Product>[] = [
    createTableColumn<Product>({
      columnId: "productName",
      renderHeaderCell: () => {
        return "Название";
      },
      renderCell: (item) => {
        return item.name;
      },
    }),
    createTableColumn<Product>({
      columnId: "productPrice",
      renderHeaderCell: () => {
        return "Цена, руб.";
      },
      renderCell: (item) => {
        if (item.price === item.salePrice) {
          return item.price;
        }

        return (
          <div style={{ display: "flex", flexDirection: "column" }}>
            <Body1Strong>{item.salePrice}</Body1Strong>
            <s>{item.price}</s>
          </div>
        );
      },
    }),
    createTableColumn<Product>({
      columnId: "makePurchase",
      renderHeaderCell: () => {
        return "";
      },
      renderCell: (item) => {
        return (
          <Purchase
            product={item}
            onPurchaseCreated={onPurchaseCreated}
            onError={onError}
          />
        );
      },
    }),
  ];

  return (
    <Card>
      <DataGrid items={products} columns={columns} getRowId={(item) => item.id}>
        <DataGridHeader>
          <DataGridRow>
            {({ renderHeaderCell }) => (
              <DataGridHeaderCell>{renderHeaderCell()}</DataGridHeaderCell>
            )}
          </DataGridRow>
        </DataGridHeader>
        <DataGridBody<Product>>
          {({ item, rowId }) => (
            <DataGridRow<Product> key={rowId}>
              {({ renderCell }) => (
                <DataGridCell>{renderCell(item)}</DataGridCell>
              )}
            </DataGridRow>
          )}
        </DataGridBody>
      </DataGrid>
    </Card>
  );
};

export { Products };
