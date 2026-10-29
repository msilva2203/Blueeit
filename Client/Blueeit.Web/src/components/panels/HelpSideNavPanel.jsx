import Block from "../Block";
import BlockHeader from "../BlockHeader";
import BlockLink from "../BlockLink";

export default function HelpSideNavPanel({selectionIndex}) {
    return (
        <>
            <Block>
                <BlockHeader>
                    Help
                </BlockHeader>
                <BlockLink content={"About"} ref={"/help/about"} isSelected={selectionIndex === 1}/>
                <BlockLink content={"Contact"} ref={"/help/contact"} isSelected={selectionIndex === 2}/>
            </Block>
        </>
    );
}