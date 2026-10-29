import Block from "../Block";
import BlockContent from "../BlockContent";
import BlockHeader from "../BlockHeader";
import BlockLink from "../BlockLink";

export default function MembersSideNavPanel({selectionIndex}) {
    return (
        <>
            <Block>
                <BlockHeader>
                    Members
                </BlockHeader>
                <BlockLink content={"All"} ref={"/members?key=none"} isSelected={selectionIndex === 1}/>
                <BlockLink content={"Newest"} ref={"/members?key=newest"} isSelected={selectionIndex === 2}/>
                <BlockLink content={"Oldest"} ref={"/members?key=oldest"} isSelected={selectionIndex === 3}/>
                <BlockLink content={"Most Messages"} ref={"/members?key=most-messages"} isSelected={selectionIndex === 4}/>
                <BlockLink content={"Today's Birthdays"} ref={"/members?key=todays-birthdays"} isSelected={selectionIndex === 5}/>
                <BlockLink content={"Staff"} ref={"/members?key=staff"} isSelected={selectionIndex === 6}/>
            </Block>
        </>
    );
}