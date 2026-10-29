import { useState } from "react";

export function usePaginatedState({initialPage = 1} = {}) {
    const [items, setItems] = useState([]);
    const [page, setPage] = useState(initialPage);
    const [totalPages, setTotalPages] = useState(1);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    function setData(data) {
        setItems(data.items);
        setTotalPages(data.totalPages);
    }

    return {
        items,
        setItems,
        page,
        setPage,
        totalPages,
        setTotalPages,
        isLoading,
        setIsLoading,
        error,
        setError,
        setData
    };
}