import "./Pagination.css";

export default function Pagination({currentPage, totalPages, setPage}) {
    let pages = [];

    const range = 5;

    const first = Math.max(currentPage - range, 1);
    const last = Math.min(currentPage + range, totalPages);

    for (let i = first; i <= last; i++) {
        pages.push(i);
    }

    const isFirstPage = currentPage <= 1;
    const isLastPage = currentPage >= totalPages;

    const isVisible = totalPages > 1;

    if (!isVisible) {
        return (
            <>
            </>
        );
    }

    return (
        <>
            <div className="blueeit-pagination">

                { !isFirstPage ? (
                    <>
                        <button onClick={() => setPage(currentPage - 1)} className="blueeit-pagination-item jump prev">
                            <h3 className="blueeit-pagination-content">
                                Prev
                            </h3>
                        </button>
                    </>
                ) : (
                    <></>
                )}

                <ul className="blueeit-pagination-main">
                    { pages.map((page, index) => (
                        <li className={`blueeit-pagination-item page ${page == currentPage ? ("current") : ("")}`}>
                            <button key={index} onClick={() => setPage(page)} className={`blueeit-pagination-btn`}>
                                <h3 className={`blueeit-pagination-content ${page == currentPage ? ("current") : ("")}`}>
                                    {page}
                                </h3>
                            </button>
                        </li>
                    ))}
                </ul>

                { !isLastPage ? (
                    <>
                        <button onClick={() => setPage(currentPage + 1)} className="blueeit-pagination-item jump next">
                            <h3 className="blueeit-pagination-content">
                                Next ›
                            </h3>
                        </button>
                    </>
                ) : (
                    <></>
                )}

            </div>
        </>
    );
}