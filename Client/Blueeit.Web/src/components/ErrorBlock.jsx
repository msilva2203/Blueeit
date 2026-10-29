import "./ErrorBlock.css";

export default function ErrorBlock({children}) {
    return (
        <>
            <div className="blueeit-block-error">
                <div className="blueeit-block-header-error">

                </div>
                {children}
            </div>
        </>
    );
}